$(function () {
    $('.lesson__form-editor').summernote({
        lang: 'ru-RU',
        height: 509,
        Width: 724,
        placeholder: "Введите код программы",
        toolbar: [
            ['view', ['codeview']],

        ]
    })
    $('.note-btn.btn.btn-light.btn-sm.btn-codeview.note-codeview-keep').click()
    // let text
    // $('.lesson__form-btn').on('click', function (e) {
    //     e.preventDefault()
    //     text = $('.lesson__form-editor').summernote('code')
    //     console.log(text);
    // })

    qqq = 0;
    setInterval(function () {
        qqq++;
        if (qqq == 1) $('#qwe').text('.');
        else if (qqq == 2) $('#qwe').text('..');
        else if (qqq == 3) $('#qwe').text('...');
        else {
            $('#qwe').empty();
            qqq = 0;
        }
    }, 500);
    $('.btn-run').on('click', function (e) {
        e.preventDefault()
        $('.loading').toggleClass('result-visible')
        $('.result-first').toggleClass('result-visible')
        setTimeout(() => {
            $('.loading').toggleClass('result-visible')
            $('.result-first').toggleClass('result-visible')
        },2000)
    })
});