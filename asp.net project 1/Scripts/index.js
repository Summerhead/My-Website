function setNewYear() {
    var date1 = new Date();
    var curYear = date1.getFullYear();
    var date2 = new Date(curYear + 1, 0, 1, 0, 0, 0, 0);
    var timeRamaining = date2 - date1;
    var d = Math.floor((timeRamaining) / 1000 / 60 / 60 / 24);
    var timeRamainingMinusD = timeRamaining - d * 24 * 60 * 60 * 1000;
    var h = Math.floor((timeRamainingMinusD) / 1000 / 60 / 60);
    var timeRamainingMinusDH = timeRamainingMinusD - h * 60 * 60 * 1000;
    var m = Math.floor((timeRamainingMinusDH) / 1000 / 60);
    var timeRamainingMinusDHS = timeRamainingMinusDH - m * 60 * 1000;
    var s = Math.floor((timeRamainingMinusDHS) / 1000);
    document.getElementById("ny").innerHTML = `До нового года осталось 
${d} ${setWord(0, d)} 
${h} ${setWord(1, h)} 
${m} ${setWord(2, m)} 
и ${s} ${setWord(3, s)}`;
    setTimeout(setNewYear, 1000);
}
setNewYear();

function setWord(n, x) {
    var words = [["дней", "день", "дня"],
    ["часов", "час", "часа"],
    ["минут", "минута", "минуты"],
    ["секунд", "секунда", "секунды"]]
    var x1 = Math.floor(x / 10) % 10;
    var x2 = x % 10;
    if (x1 === 1) {
        return words[n][0];
    }
    else {
        switch (x2) {
            case 1:
                return words[n][1];
                break;
            case 2:
            case 3:
            case 4:
                return words[n][2];
                break;
            default:
                return words[n][0];
                break;
        }
    }
}