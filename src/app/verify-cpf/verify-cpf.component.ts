import { Component, OnChanges, SimpleChanges } from '@angular/core';
@Component({
  selector: 'app-verify-cpf',
  templateUrl: './verify-cpf.component.html',
  styleUrls: ['./verify-cpf.component.css']
})
export class VerifyCpfComponent {
  protected cpf = "";
  protected isCpf = true;

  protected updateIsCpf() {
    this.isCpf = this.isValidCPF()
  }

  protected cpfChanged(event: any) {
    this.cpf = event;
    this.updateIsCpf()
  }
  protected isValidCPF() {
    if (typeof this.cpf !== "string") return false
    this.cpf = this.cpf.replace(/[\s.-]*/igm, '')
    if (this.cpf.length !== 11 || !Array.from(this.cpf).filter(e => e !== this.cpf[0]).length) {
      return false
    }
    var soma = 0
    var resto
    for (var i = 1; i <= 9; i++)
      soma = soma + parseInt(this.cpf.substring(i - 1, i)) * (11 - i)
    resto = (soma * 10) % 11
    if ((resto == 10) || (resto == 11)) resto = 0
    if (resto != parseInt(this.cpf.substring(9, 10))) return false
    soma = 0
    for (var i = 1; i <= 10; i++)
      soma = soma + parseInt(this.cpf.substring(i - 1, i)) * (12 - i)
    resto = (soma * 10) % 11
    if ((resto == 10) || (resto == 11)) resto = 0
    if (resto != parseInt(this.cpf.substring(10, 11))) return false
    return true
  }
}