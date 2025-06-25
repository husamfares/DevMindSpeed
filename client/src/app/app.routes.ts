import { Routes } from '@angular/router';
import { GameComponent } from './game/game.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = 
[
      { path: '', component: HomeComponent }, // Default route

    {
     path : "game" ,
     component : GameComponent
    }

];
