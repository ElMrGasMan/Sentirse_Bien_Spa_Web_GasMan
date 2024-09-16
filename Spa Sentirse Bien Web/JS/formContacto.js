document.getElementById('contactForm').addEventListener('submit', async function (event) {
    event.preventDefault(); // Prevenir que se recargue la página al enviar el formulario

    const nombre = document.getElementById('nombre').value;
    const apellido = document.getElementById('apellido').value;
    const emailDest = document.getElementById('email').value;
    const telefono = document.getElementById('telefono').value;
    const asuntoMens = document.getElementById('asunto').value;
    const mensajeX = document.getElementById('mensaje').value;
    const verification = document.getElementById('verification').value;

    // Verificación básica
    if (verification !== 'SPASB') {
        alert('La palabra clave de verificación es incorrecta');
        return;
    }

    const mensajeCompleto = `${apellido}, ${nombre}. Teléfono: ${telefono}. ${mensajeX}.`;

    const jsonData = {
        email: emailDest, 
        asunto: asuntoMens,
        mensaje: mensajeCompleto
    };

    try {
        const response = await fetch('https://localhost:7034/api/Contact/send', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(jsonData)
        });

        if (response.status === 200) {
            document.getElementById('successNotification').style.display = 'block';
            document.getElementById('errorNotification').style.display = 'none';
        }
    } catch (error) {
        document.getElementById('errorNotification').style.display = 'block';
        document.getElementById('successNotification').style.display = 'none';
    }
});
