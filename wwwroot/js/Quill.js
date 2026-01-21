let quill;

window.initializeQuill = function () {
    const container = document.getElementById('quill-editor');
    if (!container) return;

    quill = new Quill(container, {
        theme: 'snow',
        modules: {
            toolbar: [
                [{ header: [1, 2, 3, false] }],
                ['bold', 'italic', 'underline'],
                [{ list: 'ordered' }, { list: 'bullet' }],
                ['link'],
                ['clean']
            ]
        }
    });
};

window.getQuillContent = function () {
    return quill ? quill.root.innerHTML : '';
};

window.destroyQuill = function () {
    quill = null;
};
