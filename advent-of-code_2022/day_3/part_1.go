package main

import (
	"advent-of-code_2022/day_3/lib"
	"advent-of-code_2022/shared"
	"fmt"
	"slices"
)

type rucksack struct {
	pocket1 []uint8
	pocket2 []uint8
}

func sortRucksack(sack string) *rucksack {
	length := len(sack)
	pocket1 := make([]uint8, length/2)
	pocket2 := make([]uint8, length/2)
	for i := 0; i < length; i++ {
		if i < length/2 {
			pocket1[i] = sack[i]
		} else {
			pocket2[i-length/2] = sack[i]
		}
	}
	return &rucksack{pocket1: pocket1, pocket2: pocket2}
}

func (rucksack *rucksack) findOutlier() uint8 {
	for _, item := range rucksack.pocket1 {
		if slices.Contains(rucksack.pocket2, item) {
			return item
		}
	}
	panic("Could not find outlier in rucksack")
}

func main() {
	rucksacks := shared.ReadFileLines("day_3/input.txt")
	sortedSacks := make([]*rucksack, len(rucksacks))
	for i, rucksack := range rucksacks {
		sortedSacks[i] = sortRucksack(rucksack)
	}
	priority := uint(0)
	for _, rucksack := range sortedSacks {
		outlier := rucksack.findOutlier()
		priority += lib.GetPriority(outlier)
	}
	fmt.Println(priority)
}
