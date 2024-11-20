async function renderWord() {
    let randPos = Math.floor(Math.random() * words.length);

    if (words.length == 0) return;

    for (let i = 0; words[randPos].id == curWordId; i++) {
        randPos = Math.floor(Math.random() * words.length);
        if (i == 3) {
            break;
        }
    }

    curWordId = words[randPos].id;
    $('#word').text(words[randPos].value);
    $('#wordCardPlaceholder').html(`<div class="spinner-grow text-info" role="status"><span class="sr-only">Loading...</span></div>`);
    debugger
    const resp = await fetch(`/Learn/WordCard?wordId=${curWordId}`);
    const text = await resp.text();
    $('#wordCardPlaceholder').html(text);

    $('.btn-hint').click(() => {
        $('#hint-panel').slideDown(1000);
    });

    $('#btn-reveal').click((e) => {
        $(e.target).hide();
        $('#hints').slideUp();
        $('#carouselIndicators').slideDown();
        $('.decision-btn-row').show();
    })
}

async function updateScore(delta) {
    var resp = await fetch('/Learn/UpdateScore', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            WordId: curWordId,
            Delta: delta
        }),
    });

    let value = await resp.text();

    if (value == '3') {
        var index = words.findIndex(x => x.id = curWordId);
        words.splice(index, 1);

        if (learnedWordCount < wordCount) {
            learnedWordCount++;
            $('#learnedCount').text(learnedWordCount);
        }

        if (words.length == 0) {
            $('#word').text('Congratulations!');
            $('#wordCardPlaceholder').html(`<span>You'll be redirected shortly</span>`);
            setTimeout(() => window.location = "/Learn", 3000);
        }
    }

}
