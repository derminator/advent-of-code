package main

import (
	"advent-of-code_2022/day_2/lib"
	"advent-of-code_2022/shared"
	"fmt"
)

func getScore(round string) uint {
	score := uint(0)
	result, them := lib.ReadRound(round)
	var you uint8
	switch result {
	case 'Y':
		score += 3
	case 'Z':
		score += 6
	}

	return score
}

func main() {
	rounds := shared.ReadFileLines("day_2/input.txt")
	score := uint(0)

	for _, round := range rounds {
		score += getScore(round)
	}

	fmt.Println(score)
}
