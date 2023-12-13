import {readFileSync} from "fs";

const input = readFileSync("input.txt", {encoding: "utf8"});

const seeds = input.match(/seeds: (.*?)\n/)[1].split(" ").map(seed => parseInt(seed));

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
 * @param {Array<{dataStart: number, srcStart: number, range: number}>} map
 * @param {number} input
 */
function useMap(map, input) {
    map.forEach(value => {

    });
}

const seedsToSoil = readMap('seed-to-soil map');
const soilToFertilizer = readMap('soil-to-fertilizer map');
const fertilizerToWater = readMap('fertilizer-to-water map');
const waterToLight = readMap('water-to-light map');
const lightToTemperature = readMap('light-to-temperature map');
const temperatureToHumidity = readMap('temperature-to-humidity map');
const humidityToLocation = readMap('humidity-to-location map');


