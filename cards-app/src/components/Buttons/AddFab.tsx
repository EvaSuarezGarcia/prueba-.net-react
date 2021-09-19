import AddIcon from "@mui/icons-material/Add";
import { Fab, FabProps } from "@mui/material";
import React from "react";

const AddFab: React.FC<FabProps> = (props) => {
    return (
        <Fab
            sx={{
                position: "fixed",
                bottom: 30,
                right: 30,
            }}
            color="primary"
            aria-label="add"
            {...props}
        >
            <AddIcon />
        </Fab>
    );
};

export default AddFab;
