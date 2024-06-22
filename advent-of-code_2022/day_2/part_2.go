package main

import (
	"advent-of-code_2022/shared"
	"fmt"
)

func main() {
	rounds := shared.ReadFileLines("day_2/input.txt")
	score := uint(0)

	for _, round := range rounds {
		fmt.Println(round)
		// TODO: Score
	}

	fmt.Println(score)
}
