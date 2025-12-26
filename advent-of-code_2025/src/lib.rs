pub mod input;

pub fn concat_nums(nums: &[u8]) -> u8 {
    nums.iter().fold(0, |acc, &n| acc * 10 + n)
}
