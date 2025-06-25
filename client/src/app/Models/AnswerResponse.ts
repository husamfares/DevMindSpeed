export interface QuestionDto {
  question: string;
  submitUrl: string;
}

export interface AnswerResponse {
  result: string;
  timeTaken: number;
  nextQuestion: QuestionDto;
  currentScore: number;
}