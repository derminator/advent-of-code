package main

import (
	"advent-of-code_2022/shared"
	"fmt"
)

func readRound(round string) uint {
	score := uint(0)

	you := round[2]
	switch you {
	case 'X':
		score += 1
		break
	case 'Y':
		score += 2
		break
	case 'Z':
		score += 3
	}

	them := round[0]
	if (you - 23) == them {
		score += 3
	} else if (you == 'X' && them == 'C') || (you == 'Y' && them == 'A') || (you == 'Z' && them == 'B') {
		score += 6
	}

	return score
}

func main() {
	rounds := shared.ReadFileLines("day_2/input.txt")
	score := uint(0)

	for _, round := range rounds {
		score += readRound(round)
	}

	fmt.Println(score)
}
