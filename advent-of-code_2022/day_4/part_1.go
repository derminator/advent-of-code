package main

import (
	"advent-of-code_2022/day_4/lib"
	"fmt"
)

func checkForDuplicate(pair *lib.ElfPair) bool {
	return (pair.Elf1.Low <= pair.Elf2.Low && pair.Elf1.High >= pair.Elf2.High) ||
		(pair.Elf2.Low <= pair.Elf1.Low && pair.Elf2.High >= pair.Elf1.High)
}

func main() {
	pairs := lib.GetPairs()

	overlappingPairs := uint16(0)
	for _, pair := range pairs {
		if checkForDuplicate(pair) {
			overlappingPairs++
		}
	}
	fmt.Println("Overlapping pairs:", overlappingPairs)
}
