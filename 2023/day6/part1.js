import inputs from "./input.js";

const possibilities = inputs.map(input => {
    let wins = 0;
    for (let i = 1; i < input.time; i++) {
        const travelTime = input.time - i;
        const distance = i * travelTime;
        if (distance > input.distance) {
            wins++;
        }
    }
    return wins;
});

console.log(possibilities.reduce((prev, current) => prev * current));