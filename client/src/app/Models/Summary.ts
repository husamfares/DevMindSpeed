export interface BestScoreDto {
  question: string;
  answer: number;
  timeTaken: number;
}

export interface HistoryDto {
  question: string;
  answer: number;
  timeTaken: number;
}

export interface GameSummaryDto {
  name: string;
  difficulty: number;
  current_score: number;
  totalTimeSpent: number;
  bestScore: BestScoreDto | null;
  history: HistoryDto[];
}
