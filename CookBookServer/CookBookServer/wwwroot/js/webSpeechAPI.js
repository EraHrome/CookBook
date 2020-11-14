$(function () {

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
                console.log("Событие переключения")
                $('.btn')[1].click()
                isTimeout = !isTimeout
                setTimeout(() => { isTimeout = !isTimeout }, 750)
            }
        }
    };
    SpeechRecognition.onend = function () {
        SpeechRecognition.start();
    };
    SpeechRecognition.start();

});