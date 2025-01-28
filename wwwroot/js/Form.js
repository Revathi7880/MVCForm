
// Function to send the non empty fiters seletected by the user to filter the data
function sendFields() {
    const fields = {
        firstName: document.getElementById("firstName").value.trim(),
        lastName: document.getElementById("lastName").value.trim(),
        email: document.getElementById("email").value.trim(),
        age: document.getElementById("age").value.trim(),
        nationality: document.getElementById("nationality").value.trim(),
        occupation: document.getElementById("occupation").value.trim(),
    };

    let nonEmptyFields = {};
    for (const key in fields) {
        if (fields[key] && fields[key] != "none") {
            nonEmptyFields[key] = fields[key];
        }
    }

    $.ajax({
        url: "/Form/SearchUser",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(nonEmptyFields),
        success: function (response) {
            if (response.length > 0) {
                populateTable(response);
            }
            else {
                document.getElementById("noResult").style.display = "block";
                document.getElementById("searchTable").style.display = "none";
            }
        },
        error: function (error) {
            console.log("error", error);
        }
    });
}

// Function to populate the user list table with the returned users based on filters selected
function populateTable(users) {
    document.getElementById("noResult").style.display = "none";
    document.getElementById("searchTable").style.display = "table";
    const tableBody = document.querySelector("#searchTable tbody");
    tableBody.innerHTML = "";

    users.forEach(user => {
        const userEncoded = encodeURIComponent(JSON.stringify(user));
        const row = `
            <tr>
            <td>${user.userId}</td>
            <td>${user.firstName}</td>
            <td>${user.lastName}</td>
            <td>${user.nationality}</td>
            <td>${user.occupation}</td>
            <td>
                <div class="btn-group" role="group" aria-label="Basic example">
                  <button type="button" class="btn btn-primary" data-user="${userEncoded}" onclick="viewModal(this)">View</button>
                  <button type="button" class="btn btn-warning" onclick="userOperation(user.userId, 'delete')">Update</button>
                  <button type="button" class="btn btn-danger">Delete</button>
                </div>
            </td>
            </tr>
            `;
        tableBody.insertAdjacentHTML("beforeend", row);
    })
}

//Function that displays the modal with the user information when 'View' button is clicked
function viewModal(button) {
    const userDetails = JSON.parse(decodeURIComponent(button.getAttribute("data-user")));
    console.log(userDetails);

    const date = new Date(userDetails.dateOfBirth);
    let year = userDetails.yearCompleted;
    if (year === 0) {
        year = "";
    }
    const dateofBirth = `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
    document.getElementById('modalTitle').innerHTML = `${userDetails.firstName} ${userDetails.middleName ? userDetails.middleName + " " : ""}${userDetails.lastName} - ${userDetails.userId}`;
    document.getElementById('modalBody').innerHTML = `
        <table class="table table-striped table-bordered nameTable">
            <tbody>
                <tr>
                    <td>
                        <h5><strong>Email :</strong> ${userDetails.email}</h5>
                    </td>
                    <td>
                        <h5><strong>Phone :</strong> ${userDetails.phone}</h5>
                    </td>
                    <td>
                        <h5><strong>Date of Birth :</strong> ${dateofBirth}</h5>
                    </td>
                </tr>

                <tr>
                    <td>
                        <h5><strong>Age :</strong> ${userDetails.age}</h5>
                    </td>
                    <td>
                         <h5><strong>Nationality :</strong> ${userDetails.nationality}</h5>
                    </td>
                    <td>
                         <h5><strong>Occupation :</strong> ${userDetails.occupation}</h5>
                    </td>
                </tr>

                <tr>
                    <td>
                        <h5><strong>Street Address :</strong> ${userDetails.address1}</h5>
                    </td>
                    <td>
                        <h5><strong>Address Line 2 :</strong> ${userDetails.address2}</h5>
                    </td>
                    <td>
                         <h5><strong>City :</strong> ${userDetails.city}</h5>
                    </td>
                </tr>

                <tr>
                    <td>
                        <h5><strong>State :</strong> ${userDetails.state}</h5>
                    </td>
                    <td>
                        <h5><strong>Country :</strong> ${userDetails.country}</h5>
                    </td>
                    <td>
                        <h5><strong>Pincode :</strong> ${userDetails.pincode}</h5>
                    </td>
                </tr>

                 <tr>
                    <td>
                        <h5><strong>Degree :</strong> ${userDetails.degree}</h5>
                    </td>
                    <td>
                        <h5><strong>Institution :</strong> ${userDetails.institution}</h5>
                    </td>
                    <td>
                        <h5><strong>Year Completed :</strong> ${year}</h5>
                    </td>
                </tr>
            </tbody>
        </table>
        `;


    $(document).ready(function () {
        $('#viewModal').modal('show');
    });
}

function userOperation(userId, action) {

}
