function setUnite(set1, set2) {
    for (var item of set2) {
        set1.add(item);
    }
    return set1;
}

function removeDelimeters(text) {
    return text.replace(",", "").
        replace(";", "").
        replace("?", "").
        replace("!", "").
        replace(":", "").
        replace(".", "");
}

function remainingLetters(text) {
    for (word of removeDelimeters(text).split(" ")) {
        alert(word);
    }
}

function lettersToRemove(word) {
    let letterSet = new Set();
    let lettersToRemoveSet = new Set();
    for (var letter of word) {
        if (letterSet.has(letter)) {
            lettersToRemoveSet.add(letter);
        }
        letterSet.add(letter);
    }
    return lettersToRemoveSet;
}

function lettersToRemoveText(text) {
    let lettersToRemoveSet = new Set();
    for (var word of removeDelimeters(text).split(" ")) {
        lettersToRemoveSet = setUnite(lettersToRemoveSet, lettersToRemove(word));
    }
    return lettersToRemoveSet;
}

function removeDuplicatesText(text) {
    let result = text;
    let lettersToRemove = lettersToRemoveText(text);
    for (var letter of text) {
        if (lettersToRemove.has(letter)) {
            result = result.replace(letter, "");
        }
    }
    return result;
}

alert(removeDuplicatesText("У попа была собака"));

