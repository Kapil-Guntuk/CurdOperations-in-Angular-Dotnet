import { Component } from '@angular/core';
import { UsersDataService } from './services/users-data.service';
import { error } from 'console';
import { response } from 'express';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CurdOperations';

  users: any;
  isEditMode = false;
  editUserId: number | null = null;
  editUserData: any = {};

  constructor(private allUsers: UsersDataService) {
    this.allUsers.getAllUsers().subscribe((data) => {
      this.users = data;
    });
  }

  addUser(data: any) {
    this.allUsers.addNewUser(data).subscribe(() => {
      alert("User added successfully");
      window.location.reload();
    }, error => {
      console.error('Error adding user:', error);
      alert("Error adding user. Please try again later.");
    });
  }

  editUser(userId: number) {
    this.isEditMode = true;
    this.editUserId = userId;
    this.allUsers.getUserById(userId).subscribe((data) => {
      this.editUserData = data;
    }, error => {
      console.error('Error fetching user data for editing:', error);
      alert("Error fetching user data for editing. Please try again later.");
    });
  }

  updateUser(data: any) {
    if (this.editUserId !== null) {
      this.allUsers.updateUser(this.editUserId, data).subscribe(() => {
        alert("User updated successfully");
        window.location.reload();
        this.cancelEdit();
      }, error => {
        console.error('Error updating user:', error);
        alert("Error updating user. Please try again later.");
      });
    }
  }

  deleteUser(userId: number) {
    if (confirm('Are you sure you want to delete this user?')) {
      this.allUsers.deleteUser(userId).subscribe(() => {
          alert('User deleted successfully');
          window.location.reload();
        },
        (error) => {
          console.error('Error deleting user:', error);
          alert('Error deleting user. Please try again later.');
        }
      );
    }
  }

  cancelEdit() {
    this.isEditMode = false;
    this.editUserId = null;
  }

  getButtonLabel() {
    if (this.isEditMode) {
      return 'Update User';
    } else {
      return 'Add User';
    }
  }
}
