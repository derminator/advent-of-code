pub mod input;

pub fn concat_nums(nums: &[u8]) -> u64 {
    nums.iter().fold(0, |acc, &n| acc * 10 + n as u64)
}
