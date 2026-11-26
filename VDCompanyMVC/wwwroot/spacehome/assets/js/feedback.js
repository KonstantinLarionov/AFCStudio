const feedbackForm = document.getElementById('feedback-form');

if (feedbackForm) {
    const status = document.getElementById('feedback-status');
    const submitButton = feedbackForm.querySelector('button[type="submit"]');
    const failureMessage = 'Не удалось отправить заявку. Поля сохранены, попробуйте ещё раз.';

    function showStatus(state, message) {
        status.dataset.state = state;
        status.textContent = message;
    }

    feedbackForm.addEventListener('submit', async event => {
        event.preventDefault();
        if (submitButton.disabled) return;

        const data = new FormData(feedbackForm);
        submitButton.disabled = true;
        showStatus('pending', 'Отправляем заявку…');

        try {
            const response = await fetch(feedbackForm.action, { method: 'POST', body: data });
            const record = await response.json().catch(() => null);
            if (!response.ok || !record?.id) {
                showStatus('error', record?.message || failureMessage);
                return;
            }

            const delivery = await send_request(
                data.get('Name').trim(),
                data.get('Email').trim(),
                data.get('Subject').trim(),
                data.get('Message').trim() + '\n\nНомер заявки: ' + record.id
            );
            const result = await delivery.json().catch(() => null);
            if (!delivery.ok || result?.ok !== true) {
                showStatus('error', failureMessage);
                return;
            }

            feedbackForm.reset();
            showStatus('success', 'Заявка отправлена. Спасибо!');
        } catch {
            showStatus('error', failureMessage);
        } finally {
            submitButton.disabled = false;
        }
    });
}
