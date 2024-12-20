package main

import (
	"advent-of-code_2022/day_3/lib"
	"advent-of-code_2022/shared"
	"fmt"
	"slices"
)

type rucksackGroup struct {
	elf1, elf2, elf3 []uint8
}

func (group rucksackGroup) findBadge() uint8 {
	for _, item := range group.elf1 {
		if slices.Contains(group.elf2, item) && slices.Contains(group.elf3, item) {
			return item
		}
	}
	panic("badge not found")
}

func main() {
	rucksacks, _ := shared.ReadFileLines("day_3/input.txt")
	rucksackGroups := make([]rucksackGroup, len(rucksacks)/3)
	for i := 0; i < len(rucksacks); i += 3 {
		rucksackGroups[i/3] = rucksackGroup{
			elf1: shared.StringToChars(rucksacks[i]),
			elf2: shared.StringToChars(rucksacks[i+1]),
			elf3: shared.StringToChars(rucksacks[i+2]),
		}
	}

	badgePriorities := uint(0)
	for _, rucksackGroup := range rucksackGroups {
		badge := rucksackGroup.findBadge()
		badgePriorities += lib.GetPriority(badge)
	}

	fmt.Println("Badge priorities:", badgePriorities)
}
