const department =
{
    template:
        `
    <table class="table table-striped table-sm">
                    <thead>
                        <th>DepartmentId</th>
                        <th>Department Name</th>
                        <th>Actions</th>
                    </thead>
                    <tbody>
                        <tr v-for="dep in departments" :key="dep.DepartmentId">
                            <td>{{ dep.DepartmentId }}</td>
                            <td>{{ dep.DepartmentName }}</td>
                            <td></td>
                        </tr>
                    </tbody>
    </table>
    `
    ,
    data() {
        return {
            departments: []
        }
    },
    methods: {
        refreshData() {
            axios.get(variables.URL_API + "department")
                .then(response => {
                    this.departments = response.data;
                })
        }
    }, mounted: function () {
        this.refreshData()
    }
}