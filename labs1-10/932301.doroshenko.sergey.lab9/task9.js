document.addEventListener('DOMContentLoaded', () => {
    const display = document.getElementById('display');
    const buttons = document.querySelectorAll('.btn');

    let expression = '0';
    let isResult = false;

    function updateDisplay() {
        if (expression === '' || expression === '0') {
            display.innerHTML = '0';
            return;
        }

        if (isResult) {
            display.textContent = expression;
            return;
        }

        // Разделяем историю (серый цвет) и текущий ввод (черный цвет)
        const lastSpaceIndex = expression.lastIndexOf(' ');
        if (lastSpaceIndex !== -1) {
            const historyPart = expression.substring(0, lastSpaceIndex + 1);
            const currentPart = expression.substring(lastSpaceIndex + 1);
            display.innerHTML = `<span class="history">${historyPart}</span>${currentPart}`;
        } else {
            display.textContent = expression;
        }
    }

    function handleInput(value, action) {
        if (action === 'clear') {
            expression = '0';
            isResult = false;
        } else if (action === 'backspace') {
            if (isResult) {
                expression = '0';
                isResult = false;
            } else {
                if (expression.endsWith(' ')) {
                    expression = expression.trimEnd();
                }
                expression = expression.slice(0, -1);
                if (expression === '' || expression === '-') {
                    expression = '0';
                }
            }
        } else if (action === 'operator') {
            if (isResult) {
                isResult = false;
            }
            if (expression === '0' && value === '-') {
                expression = '-';
            } else if (expression.endsWith(' ')) {
                expression = expression.slice(0, -3) + ` ${value} `;
            } else {
                expression += ` ${value} `;
            }
        } else if (action === 'decimal') {
            if (isResult) {
                expression = '0.';
                isResult = false;
            } else {
                const parts = expression.split(' ');
                const lastPart = parts[parts.length - 1];
                if (!lastPart.includes('.')) {
                    expression += '.';
                }
            }
        } else if (action === 'equals') {
            try {
                const result = Function(`'use strict'; return (${expression})`)();
                if (isNaN(result) || !isFinite(result)) {
                    expression = 'Error';
                } else {
                    expression = String(Number(result.toFixed(8)));
                }
                isResult = true;
            } catch (e) {
                expression = 'Error';
                isResult = true;
            }
        } else if (action === 'number') {
            if (isResult || expression === '0') {
                expression = value;
                isResult = false;
            } else {
                expression += value;
            }
        }

        updateDisplay();
    }

    buttons.forEach(button => {
        button.addEventListener('click', () => {
            const value = button.textContent.trim();
            const action = button.dataset.action;
            handleInput(value, action);
        });
    });

    updateDisplay();
});