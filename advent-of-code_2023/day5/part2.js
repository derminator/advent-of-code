import {readFileSync} from "fs";
import {pool} from "workerpool";

const input = readFileSync("input.txt", {encoding: "utf8"});

const seedsIndexes = input.match(/seeds: (.*?)\n/)[1].split(" ").map(seed => parseInt(seed));
/** @type {{start: number, length: number}[]} */
const seeds = [];
for (let i = 0; i < seedsIndexes.length; i += 2) {
    seeds.push({
        start: seedsIndexes[i],
        length: seedsIndexes[i + 1]
    });
}

const workerPool = pool('./part2_worker.js');
const results = [];
const workers = seeds.map(seed => workerPool.exec('computeRange',
    [seed.start, seed.length]).then(r => {
    results.push(r);
}));
await Promise.all(workers);
void workerPool.terminate();
console.log(Math.min(...results));
