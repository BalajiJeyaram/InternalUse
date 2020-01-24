// JavaScript source code
    a = new AudioContext();
function beep(vol, freq, duration) {
        alert("beep");
    v = a.createOscillator();
    u = a.createGain();
    v.connect(u);
    v.frequency.value = freq;
    v.type = "square";
    u.connect(a.destination);
    u.gain.value = vol * 0.01;
    v.start(a.currentTime);
    v.stop(a.currentTime + duration * 0.001);
 }