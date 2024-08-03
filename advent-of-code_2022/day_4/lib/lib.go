package lib

import (
	"advent-of-code_2022/shared"
	"strconv"
	"strings"
)

type Instructions struct {
	Low  uint64
	High uint64
}

type ElfPair struct {
	Elf1, Elf2 Instructions
}

func parseInstructions(instructionText string) Instructions {
	sections := strings.Split(instructionText, "-")
	low, err := strconv.ParseUint(sections[0], 10, 64)
	shared.Catch(err)
	high, err := strconv.ParseUint(sections[1], 10, 64)
	shared.Catch(err)
	return Instructions{low, high}
}

func GetPairs() []*ElfPair {
	pairsText := shared.ReadFileLines("day_4/input.txt")
	pairs := make([]*ElfPair, len(pairsText))
	for i, pairText := range pairsText {
		jobs := strings.Split(pairText, ",")
		elf1Sections := parseInstructions(jobs[0])
		elf2Sections := parseInstructions(jobs[1])
		pairs[i] = &ElfPair{
			Elf1: elf1Sections,
			Elf2: elf2Sections,
		}
	}
	return pairs
}
