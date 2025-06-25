import { Component, inject, NgModule, OnInit } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { GameService } from '../Services/game.service';
import { CommonModule, NgIf } from '@angular/common';

@Component({
  selector: 'app-game',
  imports: [FormsModule , NgIf, CommonModule, FormsModule],
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css'] 
})
export class GameComponent{

  playerName: string = '';
  difficultyLevel: number = 1;
  message: string = '';
  question: any;
  submitUrl: string = '';
  timeStarted: string = '';
  gameStarted : boolean = false;
  SubmitAnswer : boolean = false;

  idGame : number=0;
  answer: number = 0;
  score: number = 0;
  timeTaken: number = 0;

  private gameService = inject(GameService);

   startGame() {
  this.gameStarted = true;
  this.gameService.AddPlayerData(this.playerName, this.difficultyLevel).subscribe({
    next: res => {
      this.message = res.message;
      this.question = res.question;
      this.submitUrl = res.submit_url;
      this.timeStarted = res.time_started;

      console.log('Message:', this.message);
      console.log('Question:', this.question);
      console.log('Submit URL:', this.submitUrl);
      console.log('Time Started:', this.timeStarted);
      
    },
    error: (err) => {
      console.error('Error starting game:', err);
    }
  });
}


  submitAnswer() {
    this.SubmitAnswer = true;
  this.gameService.SubmitAnswer(this.answer, this.submitUrl).subscribe({
    next: (res) => {
      // The keys below must match the JSON from the backend (camelCase)
      this.message = res.result;
      this.question = res.nextQuestion?.question;
      this.submitUrl = res.nextQuestion?.submitUrl;
      this.timeTaken = res.timeTaken;
      this.score = res.currentScore;

      console.log('Answer submitted');
      console.log('New Question:', this.question);
      console.log('Current Score:', this.score);
    },
    error: (err) => {
      console.error('Error submitting answer:', err);
    }
  });
}


gameSummary: any = null;

endGame() {
  const gameIdMatch = this.submitUrl.match(/\/submit\/(\d+)/);
  if (!gameIdMatch) return;

  const gameId = +gameIdMatch[1];

  this.gameService.EndGame(gameId).subscribe({
    next: (res) => {
      this.gameSummary = res;
      this.gameStarted = false;  // Optional: hide question UI
    },
    error: (err) => {
      console.error('Error ending game:', err);
    }
  });
}


}
