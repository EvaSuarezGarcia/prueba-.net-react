import { Button, ButtonProps } from "@mui/material";
import { ArrowDownward, ArrowUpward } from "@mui/icons-material";
import React from "react";

interface SortButtonProps extends ButtonProps {
    ascOrder: boolean | null;
    triggerSort: () => void;
    text: string;
}

const SortButton: React.FC<SortButtonProps> = (props) => {
    const { ascOrder, triggerSort, text, ...rest } = props;
    return (
        <Button
            variant="contained"
            endIcon={
                ascOrder !== null ? (
                    ascOrder ? (
                        <ArrowUpward />
                    ) : (
                        <ArrowDownward />
                    )
                ) : null
            }
            onClick={triggerSort}
            {...rest}
        >
            {text}
        </Button>
    );
};

export default SortButton;
