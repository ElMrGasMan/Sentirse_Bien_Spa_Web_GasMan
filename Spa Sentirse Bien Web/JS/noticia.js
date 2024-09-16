document.addEventListener('DOMContentLoaded', () => {
  const carrusel = document.querySelector('.carrusel');
  const prevButton = document.querySelector('.prev');
  const nextButton = document.querySelector('.next');
  let noticias = []; // Se inicializa vacío para almacenar las noticias de la API
  let index = 0;
  const noticiasVisibles = 3;

  // Función para crear un elemento de noticia
  function createNoticiaElement(noticia) {
      const noticiaElement = document.createElement('div');
      noticiaElement.classList.add('noticia_op');
      
      noticiaElement.innerHTML = `
          <div class="imagen_noticia">
              <img src="${noticia.rutaImagen}" alt="${noticia.titulo}">
          </div>
          <div class="descripcion_noticia">
              <p>Fecha: ${noticia.fechaPublicacion}</p>
              <h1>${noticia.titulo}</h1>
          </div>
      `;
      
      noticiaElement.addEventListener('click', () => {
          window.open(noticia.rutaPDF, '_blank'); // Abre el PDF en una nueva pestaña
      });

      return noticiaElement;
  }

  // Función para mostrar las noticias en el carrusel
  function mostrarNoticias() {
      carrusel.innerHTML = '';
      for (let i = 0; i < noticiasVisibles; i++) {
          const noticiaIndex = (index + i) % noticias.length;
          const noticiaElement = createNoticiaElement(noticias[noticiaIndex]);
          carrusel.appendChild(noticiaElement);
      }
  }

  prevButton.addEventListener('click', () => {
      index = (index - 1 + noticias.length) % noticias.length;
      mostrarNoticias();
  });

  nextButton.addEventListener('click', () => {
      index = (index + 1) % noticias.length;
      mostrarNoticias();
  });

  // Función para obtener noticias de la API usando fetch
  async function obtenerNoticias() {
      try {
          const response = await fetch('https://localhost:7034/api/Noticia'); // Reemplaza con la URL de tu API
          if (!response.ok) {
              throw new Error(`Error: ${response.statusText}`);
          }
          noticias = await response.json(); // Asigna las noticias obtenidas al arreglo noticias
          mostrarNoticias(); // Muestra las noticias en el carrusel
      } catch (error) {
          console.error('Error al obtener las noticias:', error);
      }
  }

  // Llamada a la función para obtener las noticias de la API al cargar la página
  obtenerNoticias();
});

