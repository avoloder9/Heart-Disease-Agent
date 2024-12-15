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
- **CSV File**: Used as a structured format for storing and managing the dataset.  

---

## 🚀 How to Use  

1. Place your medical data file (`heart.csv`) in the appropriate directory.  
2. Run the application and allow the model to train on the data.  
3. Enter patient details when prompted to receive heart disease predictions.  
4. The program will append new data entries and predictions to the CSV file.  
