import {readFileSync} from "fs";
import {worker} from "workerpool";

const input = readFileSync("input.txt", {encoding: "utf8"});

function readMap(header) {
    return input.match(new RegExp(`${header}:\n((.|\n)*?)(\n\n|$)`))[1].split("\n").map(map => {
        const parts = map.split(" ");
        return {
            destStart: parseInt(parts[0]),
            srcStart: parseInt(parts[1]),
            range: parseInt(parts[2]),
        };
    });
}

/**
 * @param {Array<{destStart: number, srcStart: number, range: number}>} map
 * @param {number} input
 * @return {number}
 */
function useMap(map, input) {
    for (const value of map) {
        const diff = input - value.srcStart;
        if (diff >= 0 && diff <= value.range) {
            return value.destStart + diff;
        }
    }
    return input;
}

const seedsToSoil = readMap('seed-to-soil map');
const soilToFertilizer = readMap('soil-to-fertilizer map');
const fertilizerToWater = readMap('fertilizer-to-water map');
const waterToLight = readMap('water-to-light map');
const lightToTemperature = readMap('light-to-temperature map');
const temperatureToHumidity = readMap('temperature-to-humidity map');
const humidityToLocation = readMap('humidity-to-location map');

function seedToLocation(seed) {
    const soil = useMap(seedsToSoil, seed);
    const fertilizer = useMap(soilToFertilizer, soil);
    const water = useMap(fertilizerToWater, fertilizer);
    const light = useMap(waterToLight, water);
    const temperature = useMap(lightToTemperature, light);
    const humidity = useMap(temperatureToHumidity, temperature);
    return useMap(humidityToLocation, humidity);
}

function computeRange(start, length) {
    let min = undefined;
    for (let i = start; i < start + length; i++) {
        const location = seedToLocation(i);
        if (min == null || location < min) {
            min = location;
        }
    }
    return min;
}

worker({
    computeRange: computeRange,
});
