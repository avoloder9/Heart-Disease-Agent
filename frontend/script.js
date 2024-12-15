document.addEventListener("DOMContentLoaded", function () {
  const selectElements = document.querySelectorAll("select");

  selectElements.forEach(function (selectElement) {
    const firstOption = selectElement.querySelector('option[value=""]');
    if (firstOption) {
      firstOption.style.display = "block";
    }

    selectElement.addEventListener("change", function () {
      if (selectElement.value !== "") {
        firstOption.style.display = "none";
      } else {
        firstOption.style.display = "block";
      }
    });
  });
});

function predictResult() {
  event.preventDefault();
  const form = document.getElementById("heartForm");
  const formData = new FormData(form);

  const input = {
    broj_godina: parseFloat(formData.get("godina")),
    spol: parseInt(formData.get("spol")),
    tip_boli: parseInt(formData.get("tipBoli")),
    krvni_pritisak: parseFloat(formData.get("pritisak")),
    holesterol: parseFloat(formData.get("holesterol")),
    secer_nataste: parseInt(formData.get("secer")),
    ecg: parseInt(formData.get("ecg")),
    max_otkucaji: parseFloat(formData.get("brojOtkucaja")),
    angina: parseInt(formData.get("angina")),
    st_depresija: parseFloat(formData.get("stDepresija")),
    nagib: parseInt(formData.get("nagib")),
    krvni_sudovi: parseInt(formData.get("krvniSudovi")),
    talasemija: parseInt(formData.get("talasemija")),
  };

  const resultParagraph = document.querySelector("p.rezultat");

  fetch("http://localhost:5103/api/HeartDisease/predict", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(input),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error("Greška u odgovoru sa servera");
      }
      return response.json();
    })
    .then((data) => {
      if (data.probability !== undefined) {
        const probability = parseFloat(
          data.probability.replace("%", "").trim()
        );
        if (!isNaN(probability)) {
          resultParagraph.textContent = `Predviđena vjerovatnoća za bolest srca je: ${probability.toFixed(
            2
          )}%`;
        } else {
          resultParagraph.textContent =
            "Nemoguće interpretirati vjerovatnoću iz odgovora servera.";
        }
      } else {
        resultParagraph.textContent =
          "Odgovor servera ne sadrži očekivanu strukturu.";
      }
      form.reset();
    })
    .catch((error) => {
      console.error("Error:", error);
      alert("Došlo je do greške prilikom obrade odgovora.");
    });
}

function trainModel() {
  event.preventDefault();
  fetch("http://localhost:5103/api/HeartDisease/train", {
    method: "POST",
  })
    .then((response) => {
      if (response.ok) {
        console.log(response);
        alert("Uspjesno treniran model");
      } else {
        return response.text().then((errorMessage) => {
          throw new Error("Greška prilikom treniranja modela: " + errorMessage);
        });
      }
    })
    .then((message) => {
      resultParagraph.textContent =
        "Treniranje modela uspješno završeno: " + message;
    })
    .catch((error) => {
      console.error("Greška:", error.message);
      resultParagraph.textContent =
        "Došlo je do greške prilikom treniranja modela.";
    });
}
