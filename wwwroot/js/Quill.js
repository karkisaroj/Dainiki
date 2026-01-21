let quill;

window.initializeQuill = function () {
    const container = document.getElementById('quill-editor');
    if (!container) {
        console.error("Quill container not found.");
        return;
    }

    try {
        quill = new Quill(container, {
            theme: 'snow',
            modules: {
                toolbar: [
                    [{ header: [1, 2, 3, false] }],
                    ['bold', 'italic', 'underline', 'strike'],
                    ['blockquote', 'code-block'],
                    [{ list: 'ordered' }, { list: 'bullet' }],
                    [{ color: [] }, { background: [] }],
                    ['link'],
                    ['clean']
                ]
            },
            placeholder: 'Write your journal entry here...'
        });

        console.log("Quill initialized.");
    } catch (e) {
        console.error("Quill init failed:", e);
    }
};

window.getQuillContent = function () {
    if (!quill) {
        console.warn("Quill not initialized.");
        return '';
    }

    return quill.root.innerHTML;
};

window.setQuillContent = function (content) {
    if (quill && content) {
        quill.clipboard.dangerouslyPasteHTML(content);
    }
};

window.clearQuillContent = function () {
    if (quill) {
        quill.setText('');
    }
};