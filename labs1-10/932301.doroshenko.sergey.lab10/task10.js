document.addEventListener("DOMContentLoaded", () => {

    const curtain = document.getElementById('curtain');
    const stage = document.getElementById('stage');

    const lampContainer = document.getElementById('lamp-container');
    const lightBeam = document.getElementById('light-beam');
    const sceneObjects = document.getElementById('scene-objects');

    const rabbit = document.getElementById('rabbit');
    const dove = document.getElementById('dove');

    // =========================================
    // Фокус 1. Занавес
    // =========================================
    curtain.addEventListener('click', () => {
        stage.style.visibility = 'visible';
        curtain.classList.add('opened');

        setTimeout(() => {
            curtain.style.display = 'none';
        }, 1000);
    });

    // =========================================
    // Фокус 2. Свет (упрощен до клика)
    // =========================================
    lampContainer.addEventListener('click', () => {
        lightBeam.classList.toggle('on');
        sceneObjects.classList.toggle('visible');
    });

    // =========================================
    // Фокус 3. Кролик и голубь
    // =========================================
    let isAnimating = false;

    function doMagicTrick() {
        if (isAnimating) return;
        isAnimating = true;

        const isRabbitVisible = rabbit.classList.contains('visible-animal');

        if (isRabbitVisible) {
            rabbit.classList.remove('visible-animal');
            rabbit.classList.add('hidden-animal');

            setTimeout(() => {
                dove.classList.remove('hidden-animal');
                dove.classList.add('visible-animal');
                isAnimating = false;
            }, 500);
        } else {
            dove.classList.remove('visible-animal');
            dove.classList.add('hidden-animal');

            setTimeout(() => {
                rabbit.classList.remove('hidden-animal');
                rabbit.classList.add('visible-animal');
                isAnimating = false;
            }, 500);
        }
    }

    rabbit.addEventListener('click', doMagicTrick);
    dove.addEventListener('click', doMagicTrick);
});