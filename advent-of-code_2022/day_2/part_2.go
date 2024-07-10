package main

import (
	"advent-of-code_2022/day_2/lib"
	"advent-of-code_2022/shared"
	"fmt"
)

func getWinningPlay(them uint8) uint8 {
	if them == 'C' {
		return 'A'
	}
	return them + 1
}

func getLossPlay(them uint8) uint8 {
	if them == 'A' {
		return 'C'
	}
	return them - 1
}

func getShapeScore(play uint8) uint {
	return uint(play - 64)
}

func getScore(round string) uint {
	score := uint(0)
	result, them := lib.ReadRound(round)
	var you uint8
	switch result {
	case 'X': // Loss
		you = getLossPlay(them)
		break
	case 'Y': // Tie
		you = them
		score += 3
		break
	case 'Z': // Win
		you = getWinningPlay(them)
		score += 6
		break
	}

	score += getShapeScore(you)

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
