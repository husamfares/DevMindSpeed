import { Component, inject, NgModule, OnInit } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { GameService } from '../Services/game.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-game',
  imports: [FormsModule , NgIf],
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




}
