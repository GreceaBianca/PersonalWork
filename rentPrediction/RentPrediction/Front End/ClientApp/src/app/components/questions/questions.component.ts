import { Component, OnInit } from '@angular/core';
import { Question } from 'src/app/models/Questions';

@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.scss']
})
export class QuestionsComponent implements OnInit {
  questions:Question[]=[
    {
      question:"Cum resetez parola daca nu sunt autentificat?",
      answer:"Pentru a reseta parola te rugam sa navighezi in pagina de Login si sa apesi butonul 'Am uitat parola', dupa care va trebui sa iti introduci adresa de email pe care vei primi o parola noua."
  },
  {
    question:"Cum modific parola daca sunt autentificat?",
    answer:"Pentru a reseta parola te rugam sa navighezi in pagina de Cont si sa apesi butonul 'Schimba parola', dupa care va trebui sa iti introduci parola curenta si parola noua pe care o doresti."
  },
  {
    question:"De ce sunt redirectionat la pagina de Login?",
    answer:"Acest eveniment se intampla atunci cand doresti sa accesezi o ruta disponibila doar utilizatorilor autentificati"
  },
  {
    question:"De ce nu am acces pentru o anumita pagina?",
    answer:"Acest eveniment se intampla atunci cand doresti sa accesezi o ruta disponibila doar pentru administratori."
  },
]
  constructor() { }

  ngOnInit() {
  }

}
