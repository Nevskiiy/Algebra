const words = ["Budi izvrstan u onom što vidiš!", "voliš.", "Zaiskri"];
let i = 0;
let timer;

function typingEffect() {
    let word = words[i].split("");

    var loopTyping = function () {
        if (word.length > 0 && i != 2) {
            document.getElementById('r1').innerHTML += word.shift();
        }
        else {
            deletingEffect();
            return false;
        };
        timer = setTimeout(loopTyping, 135);
    };
    loopTyping();
};

function deletingEffect() {
    let word = words[i].split("");
    var loopDeleting = function () {
        if (word.length > 25) {
            word.pop();
            document.getElementById('r1').innerHTML = word.join("");
        } else {
            if (words.length > (i + 1)) {
                i++;
            } else {
                return false;
            };
            if (i < 2) {
                typingEffect();
                return false;
            } else {
                typingEffect2();
                return false;
            }
        };
        timer = setTimeout(loopDeleting, 135);
    };
    loopDeleting();
};

function typingEffect2() {
    let word = words[i].split("");
    document.getElementById('r2').innerHTML = "<br/>";
    var loopTyping = function () {
        if (word.length > 0) {
            document.getElementById('r2').innerHTML += word.shift();
        }
        else {
            document.getElementById('r3').innerHTML = ".";
            deletingEffect();
            return false;
        };
        timer = setTimeout(loopTyping, 135);
    };
    loopTyping();
};

typingEffect();