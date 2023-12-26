const input = {time: 48989083, distance: 390110311121360};

let wins = 0;
for (let i = 1; i < input.time; i++) {
    const travelTime = input.time - i;
    const distance = i * travelTime;
    if (distance > input.distance) {
        wins++;
    }
}

console.log(wins);