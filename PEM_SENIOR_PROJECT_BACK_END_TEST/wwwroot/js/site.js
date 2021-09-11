// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


    $(document).on('click', '#delMainCategory', function (e) {

        e.preventDefault();

    const id = $(this).data('id');
    const numCat = $(this).data('num');
    const titleCat = $(this).data('title');
        var singularOrPlural;
  
    //alert("ID to delete: " + id + "\n" + "# of Subcategories: " + numCat);

    //clear p html element in form body for any previous message
    document.getElementById("delP").innerHTML = "";

            if (numCat > 0) {
        singularOrPlural = numCat > 1 ? "subcategories" : "subcategory";

    document.getElementById("delP").innerHTML =
    "<strong>" + titleCat + "</strong>" + " currently has " + "<strong>" + numCat + "</strong>" + " " +
    singularOrPlural + " .\n ALL the subcategories will be deleted as well!"
            }
    else
    {
        document.getElementById("delP").innerHTML = "Are you sure you want to delete " + "<strong>" + titleCat + "</strong>" + " Category?" + "Hit delete to confirm"
    }

            //load action to get form ready in case user proceeds with deletion
    $('#deleteFormClient').attr('action', 'Categories/DeletePost/' + id);
        })

