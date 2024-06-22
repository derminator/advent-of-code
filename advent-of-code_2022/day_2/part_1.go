package main

import (
	"advent-of-code_2022/day_2/lib"
	"advent-of-code_2022/shared"
	"fmt"
)

func scoreRound(round string) uint {
	score := uint(0)

	you, them := lib.ReadRound(round)

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
		score += scoreRound(round)
	}

	fmt.Println(score)
}
