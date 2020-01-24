
a = new AudioContext();
function beep(vol, freq, duration) {
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
button1.addEventListener('click', () => {
    beep(100, 520, 200);
});
button2.addEventListener('click', () => {
    beep(999, 210, 800);
    beep(999, 500, 800);
});
