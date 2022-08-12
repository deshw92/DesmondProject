const employee={template:`
    <h1>Employee Page</h1>
    <div>
        <button type="button" class="btn btn-primary m-2 float-end" data-bs-toggle="modal" data-bs-target="#exampleModal" @click="addClick()">
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-square-fill" viewBox="0 0 16 16">
            <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3a.5.5 0 0 1 1 0z"/>
            </svg>
            Add Record
        </button>
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Photo</th>
                    <th>
                        Employee Name
                        <div class="d-flex flex-row">
                            <input class="form-control m-2"
                                v-model="EmployeeNameFilter"
                                v-on:keyup="FilterFunction()"
                                placeholder="Filter..." />
                        </div>
                    </th>
                    <th>
                        Department Name
                        <div class="d-flex flex-row">
                            <input class="form-control m-2"
                                v-model="DepartmentNameFilter"
                                v-on:keyup="FilterFunction()"
                                placeholder="Filter..." />
                        </div>
                        
                    </th>
                    
                    <!--<th>Created By</th>
                    <th>Created On</th>
                    <th>Last Updated By</th>
                    <th>Last Updated On</th>-->
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="emp in employees">
                <td><img width="25" height="25" :src="PhotoPath+emp.PhotoFileName" /></td>
                    <td>{{emp.EmployeeName}}</td>
                    <td>{{emp.DepartmentName}}</td>
                    
                    <!--<td>{{emp.CreatedBy}}</td>
                    <td>{{emp.CreatedOn}}</td>
                    <td>{{emp.LastUpdatedBy}}</td>
                    <td>{{emp.LastUpdatedOn}}</td>-->
                    <td>
                        <button class="btn btn-light mr-1" data-bs-toggle="modal" data-bs-target="#exampleModal" @click="editClick(emp)">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                            </svg>
                        </button>
                        <button class="btn btn-light mr-1" @click="callDelete(emp.Id)">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash-fill" viewBox="0 0 16 16">
                                <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z"/>
                            </svg>
                        </button>
                    </td>
                </tr>
            </tbody>
        </table>

        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">{{modalTitle}}</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="d-flex flex-row bd-highlight mb-3">
                            <div class="p-2 w-50 bd-highlight">
                                <div class="input-group mb-3">
                                    <span class="input-group-text">Name</span>
                                    <input type="text" class="form-control" v-model="EmployeeName" />
                                </div>
                                <div class="input-group mb-3">
                                    <span class="input-group-text">Department</span>
                                    <select class="form-select" v-model="DepartmentId">
                                        <option v-for="dep in departments" :value="dep.Id">
                                            {{dep.DepartmentName}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="p-2 w-50 bd-highlight">
                                <img width="250" height="250" :src="PhotoPath+PhotoFileName" />
                                <input class="m-2" type="file" @change="imageUpload">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" v-if="Id==0" class="btn btn-primary" data-bs-dismiss="modal" @click="callCreate()">Create</button>
                        <button type="button" v-if="Id!=0" class="btn btn-primary" data-bs-dismiss="modal" @click="callUpdate()">Update</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

`,

    data(){
        return {
            employees:[],
            departments:[],
            modalTitle:"",
            Id:0,
            EmployeeName:"",
            DepartmentId:"",
            PhotoFileName:"anonymous.png",
            PhotoPath:variables.PHOTO_URL

        }
    },
    methods:{
        refreshData(){
            axios.get(variables.API_URL+"employee")
            .then((response)=> {
                this.employees = response.data;
            })
            axios.get(variables.API_URL+"department")
            .then((response)=> {
                this.departments = response.data;
            })
        },
        addClick(){
            this.modalTitle = "Add Employee"
            this.Id=0;
            this.EmployeeName="";
            this.DepartmentId="";
            this.PhotoFileName="anonymous.png";
        },
        editClick(emp){
            this.modalTitle="Edit Employee"
            this.Id=emp.Id;
            this.EmployeeName=emp.EmployeeName;
            this.DepartmentId=emp.DepartmentId;
            this.PhotoFileName=emp.PhotoFileName;
        },
        callCreate(){
            axios.post(variables.API_URL+"employee",{
                EmployeeName:this.EmployeeName,
                PhotoFileName:this.PhotoFileName,
                Department:{
                    Id:this.DepartmentId
                }
            }).then((response)=> {
                this.refreshData();
                alert(response.data);
            })
        },
        callUpdate(){
            axios.put(variables.API_URL+"employee",{
                Id:this.Id,
                EmployeeName:this.EmployeeName,
                PhotoFileName:this.PhotoFileName,
                Department:{
                    Id:this.DepartmentId
                }
            }).then((response)=> {
                this.refreshData();
                alert(response.data);                
            })
        },
        callDelete(Id){
            if(!confirm("Are you sure to delete?")){
                return;
            }
            axios.delete(variables.API_URL+"employee/"+Id).then((response)=> {
                this.refreshData();
                alert(response.data);                
            })
        },
        imageUpload(event){
            let formData = new FormData();
            formData.append('file',event.target.files[0]);
            axios.post(variables.API_URL+"employee/SaveFile",
                formData
            ).then((response)=> {
                this.PhotoFileName=response.data;
            })
        },
    },
    mounted:function(){
        this.refreshData();
    }
}