import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-cpf',
  templateUrl: './cpf.component.html',
  styleUrls: ['./cpf.component.css']
})

export class CpfComponent implements OnInit 
{

  @Output() valueChanged = new EventEmitter<string>();
  @Output() seeCpfChanged = new EventEmitter<boolean>();

  protected inputStyle = "color: black;"
  protected inputText = "";
  protected initialState = true;

  ngOnInit(): void {
    this.updateInput()
  }

  protected checkBoxToogle(newValue: any) {
    this.updateInput()
  }

  protected updateInput() {
    if (this.initialState) {
      this.inputText = "Escreva seu CPF..."
      this.inputStyle = "color: gray;"
      return
    }

    this.inputStyle = "color: black;"
  }

  protected cpfChanged() {
    this.updateInput()
    this.valueChanged.emit(this.inputText)
  }

  protected cpfClick() {
    if (!this.initialState)
      return

    this.initialState = false;
    this.inputText = "";
    this.updateInput();
  }

  protected cpfFocusout() {
    if (this.inputText !== "")
      return
      
    this.initialState = true
    this.updateInput()
  }
}