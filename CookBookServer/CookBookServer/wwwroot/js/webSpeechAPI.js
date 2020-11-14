$(function () {

    function getLastIndex() {
        document.querySelectorAll('.events-wrapper a').forEach(function (el, index) {
            if (el.classList.contains('selected'))
                return index;
        })
        return 0;
    }

    var keyWords = ["следующий", "следующая", "дальше", "продолжить"]
    var isTimeout = false

    if (window.SpeechRecognition || window.webkitSpeechRecognition || window.mozSpeechRecognition || window.msSpeechRecognition) {
        console.log('Браузер поддерживает данную технологию');
    } else {
        console.log('Не поддерживается данным браузером');
    }
    var SpeechRecognition = new (window.SpeechRecognition || window.webkitSpeechRecognition || window.mozSpeechRecognition || window.msSpeechRecognition)();
    SpeechRecognition.lang = "ru-RU";
    SpeechRecognition.interimResults = true;
    SpeechRecognition.onresult = function (event) {
        if (keyWords.indexOf(event.results[0][0].transcript) !== -1) {
            if (!isTimeout) {
                var refs = $('.events-wrapper').find('a');
                var index = getLastIndex() + 1;
                console.log(index)
                if (index < refs.length && index != -1) {
                    console.log("Событие переключения")
                    refs[index].click()
                    isTimeout = !isTimeout
                    setTimeout(() => { isTimeout = !isTimeout }, 5000)
                }
            }
        }
    };
    SpeechRecognition.onend = function () {
        SpeechRecognition.start();
    };
    SpeechRecognition.start();

});