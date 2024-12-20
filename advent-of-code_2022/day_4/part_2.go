package main

import (
	"advent-of-code_2022/day_4/lib"
	"fmt"
)

func main() {
	pairs := lib.GetPairs()

	overlappingPairs := uint16(0)
	for _, pair := range pairs {
		if pair.Elf1.Low <= pair.Elf2.High && pair.Elf2.Low <= pair.Elf1.High {
			overlappingPairs++
		}
	}
	fmt.Println("Overlapping pairs:", overlappingPairs)
}
