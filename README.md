# Heart Disease Prediction Agent  

## 📖 Overview  
The **Heart Disease Prediction Agent** is an AI-powered application designed to predict the likelihood of heart disease in patients. It leverages machine learning techniques to analyze key medical data, providing healthcare professionals and individuals with quick and accurate insights for better decision-making.  

---

## ✨ Features  

### 🔍 **Model Training**  
- Trains a **logistic regression** model on medical data stored in a CSV file.  

### 🩺 **Heart Disease Prediction**  
- Accepts user inputs for various medical parameters and predicts the probability of heart disease.  
- Displays results as a probability score between 0 and 100.

### 📊 **Data Augmentation**  
- Automatically appends new patient data and predictions to the existing dataset for future training and analysis.  

### 🤖 **Interactive Data Input**  
- Provides a user-friendly interface to enter medical parameters, ensuring a smooth and intuitive experience.  

---

## 🛠️ Technologies Used 

- **C# and .NET Core**: The backbone of the agent, providing robust tools for application development and seamless integration with machine learning libraries.  
- **ML.NET**: Enables the agent to build, train, and evaluate the predictive model.  
- **HTML, CSS, and JavaScript**: Used together to structure, style, and add interactivity to the frontend, enabling the creation of a responsive user interface with dynamic features such as form handling, data fetching, and real-time updates.
- **CSV File**: Used as a structured format for storing and managing the dataset.  
---

## 🚀 How to Use  

1. Place your medical data file (`heart.csv`) in the appropriate directory.  
2. Install dependencies: Open your terminal or command prompt, navigate to the project directory, and run the following command to install the necessary dependencies: **npm install**
3. Run the application: After the dependencies are installed, start the development server to run the application: **npm run dev**
4. Enter patient details when prompted to receive heart disease predictions.  
5. The program will append new data entries and predictions to the CSV file.  
