export class CreateAccountModel {
  constructor(firstName: string, lastName: string, email: string) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.birthDate = null;
  }

  firstName!: string;
  lastName!: string;
  email!: string;
  birthDate?: Date | null;
  password!: string;
  confirmPassword!: string;
}
