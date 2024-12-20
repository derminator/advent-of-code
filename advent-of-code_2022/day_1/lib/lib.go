package lib

import (
	"advent-of-code_2022/shared"
	"strconv"
)

func GetElfCals() []int {
	lines, _ := shared.ReadFileLines("day_1/input.txt")
	elfCals := make([]int, 0)
	currentElf := 0

	for _, line := range lines {
		if len(line) == 0 {
			elfCals = append(elfCals, currentElf)
			currentElf = 0
		} else {
			num, err := strconv.Atoi(line)
			shared.Catch(err)
			currentElf += num
		}
	}
	elfCals = append(elfCals, currentElf)
	return elfCals
}
