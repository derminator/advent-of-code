package main

import (
	"advent-of-code_2022/shared"
	"fmt"
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

func main() {
	stacks, instructions := shared.ReadFileLines("day_5/input.txt", "")
	fmt.Println("Stack")
	stackMap := make(map[string]stack)
	for i := len(stacks) - 1; i >= 0; i-- {
		fmt.Println(stacks[i])
	}

	fmt.Println("Instructions")
	for _, instruction := range instructions {
		fmt.Println(instruction)
	}
}
