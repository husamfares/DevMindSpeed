import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { __param } from 'tslib';
import { StartGameInfo } from '../Models/StartGameInfo';
import { AnswerResponse } from '../Models/AnswerResponse';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  baseUrl = "http://localhost:5253"
  private http = inject(HttpClient);

  AddPlayerData(playerName : string , difficultyLevel : number){
    return this.http.post<StartGameInfo>(this.baseUrl + "/start", {
      playerName: playerName,
      difficultyLevel: difficultyLevel
    });
  }


  SubmitAnswer(answer: number, submitUrl: string) {
  return this.http.post<AnswerResponse>(`${this.baseUrl}${submitUrl}`, {
    answer: answer,
  });
}

  EndGame(gameId: number) {
  return this.http.get<any>(`${this.baseUrl}/game/${gameId}/end`);
}


}
