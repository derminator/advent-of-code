package main

import (
	"advent-of-code_2022/shared"
	"strconv"
	"strings"
)

type instructions struct {
	low  uint64
	high uint64
}

type elfPair struct {
	elf1, elf2 instructions
}

func parseInstructions(instructionText string) instructions {
	sections := strings.Split(instructionText, "-")
	low, err := strconv.ParseUint(sections[0], 10, 64)
	shared.Catch(err)
	high, err := strconv.ParseUint(sections[1], 10, 64)
	shared.Catch(err)
	return instructions{low, high}
}

func (pair elfPair) checkForDuplicate() bool {
	return false // TODO
}

func main() {
	pairsText := shared.ReadFileLines("day_4/input.txt")
	pairs := make([]*elfPair, len(pairsText))
	for i, pairText := range pairsText {
		jobs := strings.Split(pairText, ",")
		elf1Sections := parseInstructions(jobs[0])
		elf2Sections := parseInstructions(jobs[1])
		pairs[i] = &elfPair{
			elf1: elf1Sections,
			elf2: elf2Sections,
		}
	}

	//TODO
}
