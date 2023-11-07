﻿using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteClone.ViewModel
{
    public class NotesVM
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }

		private Notebook selectedNotebook;

		public Notebook SelectedNotebook
		{
			get { return selectedNotebook; }
			set { 
				selectedNotebook = value;

                //Update notes in the collection using the id of the new selected notebook
                GetNotes();
			}
		}

		public ObservableCollection<Note> Notes { get; set; }

		public NewNotebookCommand NewNotebookCommand { get; set; }
		public NewNoteCommand NewNoteCommand { get; set; }

		public NotesVM()
		{
			//Define the commands related to the notes view model
			NewNotebookCommand = new NewNotebookCommand(this);
			NewNoteCommand = new NewNoteCommand(this);

			//Define initial values inside the collections displayed in the list view
			Notebooks = new ObservableCollection<Notebook>();
			Notes = new ObservableCollection<Note>();

			//Update notebooks in the collection adding the values saved previously in the database
			GetNotebooks();
		}

		public void CreateNotebook()
		{
			Notebook newNotebook = new Notebook()
			{
				Name = "New notebook"
			};
			DatabaseHelper.Insert(newNotebook);
		}

		public void CreateNote(int notebookId)
		{
			Note newNote = new Note()
			{
				Id = notebookId,
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now,
				Title = "New note"
			};
			DatabaseHelper.Insert(newNote);
		}

		private void GetNotebooks()
		{
			//Read notebooks from the database
			var notebooks = DatabaseHelper.Read<Notebook>();
			//Clear the collection
			Notebooks.Clear();
			//Add the notebooks readed in the collection
			foreach(var notebook in notebooks)
			{
				Notebooks.Add(notebook);
			}
		}

        private void GetNotes()
        {
			//If there is a selected notebook
			if(SelectedNotebook != null)
			{
                //Read notes from the database that are related to the notebook selected
                var notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
                //Clear the collection
                Notebooks.Clear();
                //Add the notes readed in the collection
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }
    }
}
