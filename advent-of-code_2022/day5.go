package main

import (
	"advent-of-code_2022/shared"
	"fmt"
	"regexp"
	"strconv"
)

type stack []string

func (s *stack) Push(v string) {
	*s = append(*s, v)
}

func (s *stack) Pop() string {
	res := (*s)[len(*s)-1]
	*s = (*s)[:len(*s)-1]
	return res
}

type Instruction struct {
	Count int // How many crates to move
	From  int // Source stack
	To    int // Destination stack
}

// Parse a single instruction string
func parseInstruction(instruction string) *Instruction {
	// Using regex to extract the numbers
	re := regexp.MustCompile(`move (\d+) from (\d+) to (\d+)`)
	matches := re.FindStringSubmatch(instruction)

	if len(matches) != 4 {
		panic(fmt.Sprintf("Invalid instruction format: %s", instruction))
	}

	count, _ := strconv.Atoi(matches[1])
	from, _ := strconv.Atoi(matches[2])
	to, _ := strconv.Atoi(matches[3])

	return &Instruction{
		Count: count,
		From:  from,
		To:    to,
	}
}

func main() {
	const MultiMove = true

	stacks, instructions := shared.ReadFileLines("../.aoc/2022/5", "")
	stackMap := make(map[int]*stack)
	parsedInstructions := make([]*Instruction, 0)

	// First, find the line with stack numbers (should be the last line of stacks)
	var numbersLine string
	for i := len(stacks) - 1; i >= 0; i-- {
		if containsStackNumbers(stacks[i]) {
			numbersLine = stacks[i]
			break
		}
	}

	maxStack := 0
	// Initialize stacks using the numbers found
	for i := 0; i < len(numbersLine); i++ {
		if numbersLine[i] >= '1' && numbersLine[i] <= '9' {
			stackKey, _ := strconv.Atoi(string(numbersLine[i]))
			if stackKey > maxStack {
				maxStack = stackKey
			}
			stackMap[stackKey] = &stack{}
		}
	}

	// Read stack items from bottom to top (excluding the numbers line)
	for i := len(stacks) - 2; i >= 0; i-- {
		line := stacks[i]
		for key := 1; key <= maxStack; key++ {
			// Calculate the correct position in the line for this stack
			stackIdx := 1 + (key-1)*4

			// Make sure we don't go out of bounds
			if stackIdx < len(line) {
				// If there's a crate (letter between brackets), add it to the stack
				if line[stackIdx] != ' ' {
					stackMap[key].Push(string(line[stackIdx]))
				}
			}
		}
	}

	for _, instruction := range instructions {
		parsedInstructions = append(parsedInstructions, parseInstruction(instruction))
	}

	for _, instruction := range parsedInstructions {
		if MultiMove {
			move := stack{}
			for i := 0; i < instruction.Count; i++ {
				move.Push(stackMap[instruction.From].Pop())
			}
			for len(move) > 0 {
				stackMap[instruction.To].Push(move.Pop())
			}
		} else {
			for i := 0; i < instruction.Count; i++ {
				stackMap[instruction.To].Push(stackMap[instruction.From].Pop())
			}
		}
	}

	for i := 1; i <= maxStack; i++ {
		fmt.Print(stackMap[i].Pop())
	}
	fmt.Println()
}

// Helper function to check if a line contains stack numbers
func containsStackNumbers(line string) bool {
	for _, char := range line {
		if char >= '1' && char <= '9' {
			return true
		}
	}
	return false
}
